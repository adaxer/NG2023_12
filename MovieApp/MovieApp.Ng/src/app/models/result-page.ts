export interface ResultPage<T> {
  pageSize: number;
  page: number;
  totalCount:number;
  data: T[];
}

