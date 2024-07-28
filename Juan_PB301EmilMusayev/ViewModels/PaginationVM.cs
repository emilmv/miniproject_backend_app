namespace Juan_PB301EmilMusayev.ViewModels
{
    public class PaginationVM<T>:List<T>
    {
        public PaginationVM(List<T>items,int currentPage,int pageCount)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;
            this.AddRange(items);

            int start = currentPage - 2;
            int end = currentPage + 2;
            if (start <= 0)
            {
                end=end-(start-1);
                start = 1;
            }
            if(end>pageCount)
            {
                end=pageCount;
                start = end - 4;
            }
            Start=start;
            End=end;
            
        }
        public int CurrentPage { get; }
        public int PageCount { get; }
        public bool HasNextPage => CurrentPage < PageCount;
        public bool HasPreviousPage => CurrentPage > 1;
        public int Start { get; }
        public int End { get; }

        public static PaginationVM<T> Create(IQueryable<T>query,int page, int take)
        {
            var datas = query.Skip((page - 1) * take).Take(take).ToList();
            var productCount=query.Count();
            var pageCount=(int)Math.Ceiling((decimal)productCount/take);
            return new PaginationVM<T>(datas,page,pageCount);
        }
    }
}
