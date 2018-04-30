import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import { OrganizationService } from '../../../../../shared/services/organization.service';
import { ShipService } from '../../../../../shared/services/ship.service';



@Component({
    selector: 'app-search-organization',
    templateUrl: './search-organization.component.html',
    styleUrls: ['./search-organization.component.css'],
    providers: [OrganizationService]
})
export class SearchOrganizationComponent implements OnInit {

    organizationModel: any;
    organizationSelected = false;

    searching = false;
    searchFailed = false;
    hideSearchingWhenUnsubscribed = new Observable(() => () => this.searching = false);

    constructor(private organizationService: OrganizationService, private shipService: ShipService) { }

    search = (text$: Observable<string>) =>
        text$
            .debounceTime(300)
            .distinctUntilChanged()
            .do((term) => {
                this.searchFailed = false;
                if (term.length >= 2) this.searching = true;
            })
            .switchMap(term => term.length < 2 ? [] :
                this.organizationService.search(term)
            )
            .do((text$) => {
                this.searching = false;
                if (text$.length == 0) {
                    this.searchFailed = true;
                }
            })
            .merge(this.hideSearchingWhenUnsubscribed)

    formatter = (x: { organizationId: string }) => x.organizationId;

    selectOrganization($event) {
        this.organizationSelected = true;
        this.shipService.setOrganizationData($event.item);
    }

    deselectOrganization() {
        this.organizationSelected = false;
        this.organizationModel = null;
        this.shipService.setOrganizationData(this.organizationModel);
    }

    ngOnInit() {
    }
}
