import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { PERSON_ON_BOARD_TYPES } from 'app/shared/constants/enumValues';
import { PortCallFalPersonOnBoardService } from 'app/shared/services/port-call-fal-person-on-board.service';
import { Subscription } from 'rxjs/Subscription';


@Component({
  selector: 'app-crew-listing-table',
  templateUrl: './crew-listing-table.component.html',
  styleUrls: ['./crew-listing-table.component.css']
})
export class CrewListingTableComponent implements OnInit, OnDestroy {

  @Input() iconPath: string;
  @Input() portCallId: number;

  crewDataSubscription: Subscription;

  public crew: any = [];
  public headerText: string;

  // Smart table
  tableSettings = {
    mode: 'external',
    actions: false,
    attr: {
      class: 'table table-bordered'
    },
    noDataMessage: 'No crew members found.',
    columns: {
      familyName: {
        title: 'Family Name'
      },
      givenName: {
        title: 'Given Name'
      },
      rankName: {
        title: 'Rank or Rating'
      },
      nationality: {
        title: 'Nationality',
      },
      dateOfBirth: {
        title: 'Date of Birth',
      },
      placeOfBirth: {
        title: 'Place of Birth',
      },
      identityDocumentListingModel: {
        title: 'Identity Document (Type)',
      },
      issuingNation: {
        title: 'Issuing Nation',
      },
      expiryDate: {
        title: 'Expiry Date',
        valuePrepareFunction: (value) => {
          return value ? value : 'N/A';
        }
      },
    }
  };

  constructor(private personOnBoardService: PortCallFalPersonOnBoardService) { }

  ngOnInit() {

    if (this.portCallId) {
      this.crewDataSubscription = this.personOnBoardService.getCrewListByPortCallId(this.portCallId)
        .finally(() => {
          this.crewDataSubscription = this.personOnBoardService.getOverviewByPortCall(this.portCallId, PERSON_ON_BOARD_TYPES.CREW)
            .subscribe(summary => {
              this.headerText = 'Crew Info - No. of Crew: ' + summary.numberOfPobs;
            }, error => {
              this.headerText = 'Crew Info';
            });
        })
        .subscribe(
          crewList => {
            if (crewList) {
              this.crew = crewList;
            }
          }
        );
    }
  }

  ngOnDestroy() {
    this.crewDataSubscription.unsubscribe();
  }

}
