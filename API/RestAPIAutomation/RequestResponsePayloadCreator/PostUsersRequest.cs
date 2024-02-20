

using APIAutomation.RestAPIAutomation.JsonSchema.Users;

namespace APIAutomation.RestAPIAutomation.RequestPayloadCreator
{
    public class PostUsersRequest
    {
        public static PostUsers createPayload(string name, string job) 
        {
            PostUsers requestPayload = new PostUsers()
            {
                name = name,
                job = job,
                createdAt = null,
                id=null
            };
            return requestPayload;
        }
    }
}
