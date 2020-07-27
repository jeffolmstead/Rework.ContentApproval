# Rework.ContentApproval
Extends Orchard Core content management with a content approval process.

## Steps to Utilize
1. Enable Feature "Rework: Content Approval"
2. Add "Content Approval" part to the content type you want to have function under an approval process
3. Users with "Publish" permissions will now see this on the content type:
![image](https://user-images.githubusercontent.com/1848585/88558342-5f3f1f00-cff9-11ea-8833-e10e6377c75d.png)
4. For users who need to "Request Approval", navigate to their Role (e.g. Contributor) and check "Allow" for "Request content approval". This user should NOT have "Publish" permissions for this content type.
5. Users with "Request content approval" permissions will now see this on the content type:
![image](https://user-images.githubusercontent.com/1848585/88558250-47679b00-cff9-11ea-851a-6d03282c7794.png)

## Workflows
If you have workflows enabled, you will also have access to two new events:
1. Approval Request - fires when content has requested approval
2. Approval Response - fires when content has been responded

These are often used to send out email notifications to the alternate party in the approval process.

## Index Support
In addition, there is an index that is managed as part of this module called "Content Approval Part Index". This could be used in generating lists via the query module. 
