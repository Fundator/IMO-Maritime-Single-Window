import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { of } from "rxjs/observable/of";
import { BehaviorSubject, Subject } from 'rxjs';
import { ShipModel } from "../models/ship-model";
import { SearchService } from "./search.service";

@Injectable()
export class ShipService {
    constructor(private http: Http) {
        this.searchService = new SearchService(http);
        this.actionUrl = 'api/ship/search';
        this.shipTypeUrl = 'api/shiptype/getall';
        this.hullTypeUrl = 'api/shiphulltype/getall';
        this.lengthTypeUrl = 'api/shiplengthtype/getall';
        this.breadthTypeUrl = 'api/shipbreadthtype/getall';
        this.powerTypeUrl = 'api/shippowertype/getall';
        this.shipSourceUrl = 'api/shipsource/getall';
        this.registerShipUrl = 'api/ship/register';
    }

    private searchService: SearchService;
    private actionUrl: string;
    private shipTypeUrl: string;
    private hullTypeUrl: string;
    private lengthTypeUrl: string;
    private breadthTypeUrl: string;
    private powerTypeUrl: string;
    private shipSourceUrl: string;
    private registerShipUrl: string;

    private companyDataSource = new BehaviorSubject<any>(null);
    companyData$ = this.companyDataSource.asObservable();

    private countryDataSource = new BehaviorSubject<any>(null);
    countryData$ = this.countryDataSource.asObservable();

    private shipFlagCodeDataSource = new BehaviorSubject<any>(null);
    shipFlagCodeData$ = this.shipFlagCodeDataSource.asObservable();

    registerShip(newShip: any) {
        return this.http.post(this.registerShipUrl, newShip)
                .map(res => res.json());
    }    

    setCompanyData(data) {
        this.companyDataSource.next(data);
    }

    setCountryData(data) {
        this.countryDataSource.next(data);
    }

    setShipFlagCodeData(data) {
        this.shipFlagCodeDataSource.next(data);
        console.log(data);
    }

    search(term: string) {
        return this.searchService.search(this.actionUrl, term);
    }

    getShipTypes() {
        return this.http.get(this.shipTypeUrl)
                .map(res => res.json());
    }

    getHullTypes() {
        return this.http.get(this.hullTypeUrl)
                .map(res => res.json());
    }

    getLengthTypes() {
        return this.http.get(this.lengthTypeUrl)
                .map(res => res.json());
    }

    getBreadthTypes() {
        return this.http.get(this.breadthTypeUrl)
                .map(res => res.json());
    }

    getPowerTypes() {
        return this.http.get(this.powerTypeUrl)
                .map(res => res.json());
    }

    getShipSources() {
        return this.http.get(this.shipSourceUrl)
                .map(res => res.json());
    }
    
}