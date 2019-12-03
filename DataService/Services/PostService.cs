using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class PostService : IPostService
    {
        public Post GetPost(int postId)
        {
            using var db = new StackOverflowContext();
            var post = db.Posts.Find(postId);
            
            return post;
        }

        public List<Post> GetPosts(int[] postIds)
        {
            using var db = new StackOverflowContext();
            var posts = db.Posts.Where(x => postIds.Contains(x.PostId)).Select(x => x).ToList();

            return posts;
        }

        public List<WordWeight> GetWordCloud(int postId)
        {
            using var db = new StackOverflowContext();
            //var wordCloud = db.Weighted_Inverted_Index.FromSqlRaw("select wordcloud({0});", postId).ToList();
            var wordCloud = db.Weighted_Inverted_Index.Where(x => x.PostId == postId).Select(x => x.MultiplyWeight()).ToList();
            
            return wordCloud;
        }
    }
}