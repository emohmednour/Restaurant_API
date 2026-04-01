

namespace Restaurants.Application.Common;

public  class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalcount,int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemCount = totalcount;
        TotalPage = (int)Math.Ceiling(totalcount / (double)pageSize);
        ItemFrom = pageSize * (pageNumber - 1) +1;
        ItemTo =ItemFrom + pageSize -1;

    }
    public IEnumerable<T> Items { get; set; }
       
       public int TotalPage { get; set; }
    public int TotalItemCount { get; set; }
    public int ItemFrom { get; set; }
    public int ItemTo { get; set; }




}

