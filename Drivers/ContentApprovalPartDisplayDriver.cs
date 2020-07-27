using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using Rework.ContentApproval.Common;
using Rework.ContentApproval.Models;
using Rework.ContentApproval.ViewModels;
using Rework.ContentApproval.Workflows.Activities;
using System.Threading.Tasks;

namespace Rework.ContentApproval.Drivers
{
    public class ContentApprovalPartDisplayDriver : ContentPartDisplayDriver<ContentApprovalPart>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public ContentApprovalPartDisplayDriver(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public override async Task<IDisplayResult> EditAsync(ContentApprovalPart part, BuildPartEditorContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var baseShapeName = GetEditorShapeType(context);

            // Have to do Publish Content permissions first
            if (await _authorizationService.AuthorizeAsync(httpContext?.User, OrchardCore.Contents.Permissions.PublishContent, part.ContentItem))
            {
                var shapeName = $"{ baseShapeName }_ApprovalResponse";
                return Initialize<ApprovalResponseViewModel>(shapeName,
                    model => PopulateApprovalResponseViewModel(part, model))
                .Location("Actions:First");
            }
            else if (await _authorizationService.AuthorizeAsync(httpContext?.User, Permissions.RequestApproval, part.ContentItem))
            {
                var shapeName = $"{ baseShapeName }_ApprovalRequest";
                return Initialize<ApprovalRequestViewModel>(shapeName,
                    model => PopulateApprovalRequestViewModel(part, model))
                .Location("Actions:Last");
            }
            
            return null;
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentApprovalPart part, IUpdateModel updater)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // Have to do Publish Content permissions first
            if (await _authorizationService.AuthorizeAsync(httpContext?.User, OrchardCore.Contents.Permissions.PublishContent, part.ContentItem))
            {
                var viewModel = new ApprovalResponseViewModel();

                await updater.TryUpdateModelAsync(viewModel, Prefix);

                if (httpContext.Request.Form["submit.Publish"] == "submit.Publish"
                    || httpContext.Request.Form["submit.Publish"] == "submit.PublishAndContinue")
                {
                    part.Status = Settings.ContentApproved;
                    part.NotificationNeeded = nameof(ApprovalResponseEvent);
                }
                else if (httpContext.Request.Form["submit.Publish"] == "submit.Approved")
                {
                    part.Status = Settings.ContentApproved;
                    part.Notes = viewModel.Notes;
                    part.NotificationNeeded = nameof(ApprovalResponseEvent);
                }
                else if (httpContext.Request.Form["submit.Save"] == "submit.NeedsRevised")
                {
                    part.Status = Settings.ContentNeedsRevised;
                    part.Notes = viewModel.Notes;
                    part.NotificationNeeded = nameof(ApprovalResponseEvent);
                }
                else if (httpContext.Request.Form["submit.Save"] == "submit.Rejected")
                {
                    part.Status = Settings.ContentRejected;
                    part.Notes = viewModel.Notes;
                    part.NotificationNeeded = nameof(ApprovalResponseEvent);
                }
            }
            else if (await _authorizationService.AuthorizeAsync(httpContext?.User, Permissions.RequestApproval, part.ContentItem))
            {
                var viewModel = new ApprovalRequestViewModel();
                
                await updater.TryUpdateModelAsync(viewModel, Prefix);

                if (httpContext.Request.Form["submit.Save"] == "submit.RequestApproval")
                {
                    part.Status = Settings.ContentApprovalRequested;
                    part.Notes = viewModel.Notes;
                    part.NotificationNeeded = nameof(ApprovalRequestEvent);
                }
            }
            

            return Edit(part);
        }

        private void PopulateApprovalRequestViewModel(ContentApprovalPart part, ApprovalRequestViewModel viewModel)
        {
            viewModel.Status = part.Status;
            viewModel.Notes = part.Notes;
        }

        private void PopulateApprovalResponseViewModel(ContentApprovalPart part, ApprovalResponseViewModel viewModel)
        {
            viewModel.Status = part.Status;
            viewModel.Notes = part.Notes;
        }
    }
}
