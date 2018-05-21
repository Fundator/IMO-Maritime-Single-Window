import { Component, OnInit } from '@angular/core';
import { FormMetaData } from '../../../../../../../shared/models/form-meta-data.interface';
import { PortCallDetailsModel } from '../../../../../../../shared/models/port-call-details-model';
import { PortCallService } from '../../../../../../../shared/services/port-call.service';

@Component({
  selector: 'app-save-details',
  templateUrl: './save-details.component.html',
  styleUrls: ['./save-details.component.css']
})
export class SaveDetailsComponent implements OnInit {
  detailsModel: PortCallDetailsModel = new PortCallDetailsModel();
  reportingModel: any;
  crewPassengersAndDimensionsModel: any;
  purposeModel: any;
  otherPurposeName: any;

  reportingFound: boolean;
  crewPassengersAndDimensionsFound: boolean;
  purposeFound: boolean;

  crewPassengersAndDimensionsMeta: FormMetaData = { valid: true };

  dataIsPristine: boolean = true;

  constructor(private portCallService: PortCallService) { }

  ngOnInit() {

    this.portCallService.detailsPristine$.subscribe(
      detailsDataIsPristine => {
        this.dataIsPristine = detailsDataIsPristine;
      }
    );
    // Database Identification
    this.portCallService.detailsIdentificationData$.subscribe(
      identificationData => {
        if (identificationData) {
          this.detailsModel.portCallDetailsId = identificationData.portCallDetailsId;
          this.detailsModel.portCallId = identificationData.portCallId;
        }
      }
    )
    // Reporting
    this.portCallService.reportingForThisPortCallData$.subscribe(
      reportingData => {
        if (reportingData) {
          this.detailsModel.reportingBunkers = reportingData.reportingBunkers;
          this.detailsModel.reportingCargo = reportingData.reportingCargo;
          this.detailsModel.reportingCrew = reportingData.reportingCrew;
          this.detailsModel.reportingHazmat = reportingData.reportingHazmat;
          this.detailsModel.reportingPax = reportingData.reportingPax;
          this.detailsModel.reportingShipStores = reportingData.reportingShipStores;
          this.detailsModel.reportingWaste = reportingData.reportingWaste;
        }
      }
    );
    // Crew, passengers, and dimensions
    this.portCallService.crewPassengersAndDimensionsData$.subscribe(
      cpadData => {
        if (cpadData) {
          this.crewPassengersAndDimensionsModel = cpadData;
          this.detailsModel.numberOfCrew = cpadData.numberOfCrew;
          this.detailsModel.numberOfPassengers = cpadData.numberOfPassengers;
          this.detailsModel.airDraught = cpadData.airDraught;
          this.detailsModel.actualDraught = cpadData.actualDraught;
        }
      }
    );
    // Purpose
    this.portCallService.portCallPurposeData$.subscribe(
      purposeData => {
        if (purposeData) {
          this.purposeFound = true;
          this.purposeModel = purposeData;
        }
      }
    );

    this.portCallService.otherPurposeName$.subscribe(
      otherNameData => {
        this.otherPurposeName = otherNameData;
      }
    )

    this.portCallService.crewPassengersAndDimensionsMeta$.subscribe(
      cpadMetaData => {
        this.crewPassengersAndDimensionsMeta = cpadMetaData;
      }
    );
  }

  saveDetails() {
    if (this.crewPassengersAndDimensionsMeta.valid) {
      this.detailsModel.numberOfCrew = this.crewPassengersAndDimensionsModel.numberOfCrew;
      this.detailsModel.numberOfPassengers = this.crewPassengersAndDimensionsModel.numberOfPassengers;
      this.detailsModel.airDraught = this.crewPassengersAndDimensionsModel.airDraught;
      this.detailsModel.actualDraught = this.crewPassengersAndDimensionsModel.actualDraught;
      this.portCallService.saveDetails(this.detailsModel, this.purposeModel, this.otherPurposeName);
    }
  }
}