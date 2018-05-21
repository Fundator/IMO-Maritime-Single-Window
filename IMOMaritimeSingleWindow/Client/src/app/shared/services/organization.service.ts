import { Injectable } from "@angular/core";
import { Http, RequestOptions } from "@angular/http";
import { BehaviorSubject, Observable } from "rxjs";
import { OrganizationModel } from "../models/organization-model";
import { AuthRequest } from "./auth.request.service";
import { SearchService } from "./search.service";

@Injectable()
export class OrganizationService {
    private searchService: SearchService;
    private organizationSearch: string;
    private registerOrganizationUrl: string;
    private getOrganizationTypesUrl: string;
    private getOrganizationForUserUrl: string;

    constructor(private http: Http, private authRequestService: AuthRequest) {
        this.searchService = new SearchService(http);
        this.organizationSearch = 'api/organization/search';
        this.registerOrganizationUrl = 'api/organization/register';
        this.getOrganizationTypesUrl = 'api/organizationtype/getall';
        this.getOrganizationForUserUrl = 'api/organization/foruser';
    }


    private organizationDataSource = new BehaviorSubject<any>(null);
    organizationData$ = this.organizationDataSource.asObservable();

    setOrganizationData(data) {
        this.organizationDataSource.next(data);
    }

    public registerOrganization(newOrganization: OrganizationModel) {
        var auth_headers = this.authRequestService.GetHeaders();
        let options = new RequestOptions({ headers: auth_headers });
        return this.http.post(this.registerOrganizationUrl, newOrganization, options)
            .map(res => res.json());
    }

    public search(term: string) {
        if (term.length < 2) {
            return Observable.of([]);
        }
        return this.searchService.search(this.organizationSearch, term);
    }

    public getOrganizationTypes() {
        return this.http.get(this.getOrganizationTypesUrl)
            .map(res => res.json());
    }

    public getOrganizationForUser() {
        let auth_headers = this.authRequestService.GetHeaders();
        let options = new RequestOptions({ headers: auth_headers });
        let uri: string = this.getOrganizationForUserUrl;
        return this.http.get(uri, options)
            .map(res => res.json());
    }

}