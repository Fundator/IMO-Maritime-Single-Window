import { Component, OnInit } from '@angular/core';
import { ContentService } from '../../../../../shared/services/content.service';
import { PortCallService } from '../../../../../shared/services/port-call.service';
import { PortCallDetailsModel } from '../../../../../shared/models/port-call-details-model';
import { FormMetaData } from '../../../../../shared/models/form-meta-data.interface';

@Component({
  selector: 'app-save-and-send',
  templateUrl: './save-and-send.component.html',
  styleUrls: ['./save-and-send.component.css']
})
export class SaveAndSendComponent implements OnInit {

  detailsDataIsPristine: boolean = true;
  detailsIdentificationModel: any;
  crewPassengersAndDimensionsModel: any;
  purposeModel: any;
  reportingModel: any;
  otherPurposeName: string = "";
  detailsMeta: FormMetaData;
  detailsModel: PortCallDetailsModel = new PortCallDetailsModel();

  portCallStatus: string;
  STATUS_ACTUAL = "Actual";

  constructor(private contentService: ContentService, private portCallService: PortCallService) { }

  ngOnInit() {
    this.portCallService.detailsPristine$.subscribe(
      detailsDataIsPristine => {        
        this.detailsDataIsPristine = detailsDataIsPristine;
      }
    );
    this.portCallService.detailsIdentificationData$.subscribe(
      detailsIdentificationData => {
        if (detailsIdentificationData) this.detailsIdentificationModel = detailsIdentificationData;
      }
    );
    this.portCallService.crewPassengersAndDimensionsData$.subscribe(
      cpadData => {
        if (cpadData) this.crewPassengersAndDimensionsModel = cpadData;
      }
    );
    this.portCallService.reportingForThisPortCallData$.subscribe(
      reportingData => {
        if (reportingData) this.reportingModel = reportingData;
      }
    );
    this.portCallService.portCallPurposeData$.subscribe(
      purposeData => {
        if (purposeData) this.purposeModel = purposeData;
      }
    );
    this.portCallService.otherPurposeName$.subscribe(
      otherPurposeNameData => {
        if (otherPurposeNameData) this.otherPurposeName = otherPurposeNameData;
      }
    );
    this.portCallService.crewPassengersAndDimensionsMeta$.subscribe(
      metaData => {
        this.detailsMeta = metaData;
      }
    );
    this.portCallService.portCallStatusData$.subscribe(
      statusData => {
        this.portCallStatus = statusData;
      }
    ); 
  }

  saveDetails() {
    this.detailsModel.portCallDetailsId = this.detailsIdentificationModel.portCallDetailsId;
    this.detailsModel.portCallId = this.detailsIdentificationModel.portCallId;
    this.detailsModel.numberOfCrew = this.crewPassengersAndDimensionsModel.numberOfCrew;
    this.detailsModel.numberOfPassengers = this.crewPassengersAndDimensionsModel.numberOfPassengers;
    this.detailsModel.airDraught = this.crewPassengersAndDimensionsModel.airDraught;
    this.detailsModel.actualDraught = this.crewPassengersAndDimensionsModel.actualDraught;
    this.detailsModel.reportingBunkers = this.reportingModel.reportingBunkers;
    this.detailsModel.reportingCargo = this.reportingModel.reportingCargo;
    this.detailsModel.reportingCrew = this.reportingModel.reportingCrew;
    this.detailsModel.reportingHazmat = this.reportingModel.reportingHazmat;
    this.detailsModel.reportingPax = this.reportingModel.reportingPax;
    this.detailsModel.reportingShipStores = this.reportingModel.reportingShipStores;
    this.detailsModel.reportingWaste = this.reportingModel.reportingWaste;
    this.portCallService.saveDetails(this.detailsModel, this.purposeModel, this.otherPurposeName);
    console.log("META: ", this.detailsMeta.valid, "\nPRISTINE: ", this.detailsDataIsPristine);
  }

  send() {
    this.portCallService.updatePortCallStatusActual(this.detailsIdentificationModel.portCallId);
    this.goBack();
  }

  goBack() {
    this.contentService.setContent("Port Call");
  }

}
