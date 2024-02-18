using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumApp.Models;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllAsync();
        Task AddAsync(PostViewModel model);
        Task EditAsync(PostViewModel model);
        Task DeleteAsync(int id);
    }
}
