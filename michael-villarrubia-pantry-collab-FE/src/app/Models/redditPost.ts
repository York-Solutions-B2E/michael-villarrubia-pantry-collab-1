export class RedditPost {
  constructor(
    public title: string,
    public link: string,
    public thumbnail: string,
    public ingredients: string,
    public instructions: string,
    public utcCreated: number,
    public found: boolean
  ) {}
}
