import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class SearchService {
  constructor(private http: Http) { }

  public search(baseUrl: string, term: string, amount = 10) {
    const encodedTerm: string = encodeURIComponent(term);
    const uri: string = [baseUrl, encodedTerm, amount].join('/');

    return this.http.get(uri).map(res => res.json());
  }
}
