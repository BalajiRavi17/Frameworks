using APIAutomation.RestAPIAutomation.JsonSchema.Users;

namespace APIAutomation.RestAPIAutomation.Comparers
{
    public class PostUserComparer
    {
        public static bool compareResults(PostUsers x, PostUsers y) 
        { 
            if (x == null || y== null) return false;
            if (x.name != y.name) return false;
            if (x.job!=y.job) return false;
            //if(string.IsNullOrEmpty(y.createdAt.ToString())) return false;
            //if (string.IsNullOrEmpty(y.id)) return false;
            return true;
        }
    }
}
