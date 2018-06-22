import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';
import { CONTENT_NAMES } from 'app/shared/constants/content-names';
import { ConstantsService } from 'app/shared/services/constants.service';
import { ContentService } from 'app/shared/services/content.service';
import { LocationService } from 'app/shared/services/location.service';

@Component({
  selector: 'app-location-button-row',
  templateUrl: './location-button-row.component.html',
  styleUrls: ['./location-button-row.component.css'],
  providers: [ConstantsService]
})
export class LocationButtonRowComponent implements ViewCell, OnInit {

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() edit: EventEmitter<any> = new EventEmitter();

  locationData: any[];

  constructor(
    private locationService: LocationService,
    private contentService: ContentService
  ) { }

  ngOnInit() {
    this.locationService.locationData$.subscribe(
      results => {
        if (results) {
          this.locationData = results;
        }
      }
    );
  }

  onEditClick() {
    this.setContent(CONTENT_NAMES.REGISTER_LOCATION);
  }

  private setContent(content: string) {
    this.setLocation(content);
  }

  setLocation(content) {
    this.contentService.setLoadingScreen(true, 'location.gif', 'Loading');
    this.locationService.setLocationData(this.rowData.locationModel);
    this.contentService.setContent(content);
  }

}
