using System.Dynamic;

namespace rawdata_portfolioproject_2
{
    public interface IDataService
    {
        // THESE SUGGESTIONS ARE ONLY PRELIMINARY. ADD/REMOVE/REFINE FUNCTIONS AS NEEDED
        
        // PROFILE FUNCTIONS
        // create/read/update/delete (CRUD) profile
        // login/out
        // CRUD bookmark
        
        // QA FUNCIONS
        // get post (single or multiple) (by id or searchwords)
        // get question (single) (by postid)
        // get link (multiple) by (by postid and linktoid)
        // get answer (multiple) (by postid and answertoid)
        // get comment (multiple) (by postid)
        // get user (single) (by userid)
        
        Post GetPost(int postId);
    }
}