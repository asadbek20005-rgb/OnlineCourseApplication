using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Public.Article.Interfaces;

public interface IArticleService : IStatusGeneric
{
    Task<PaginationModel<ArticleDto>> GetAllAsync();
}