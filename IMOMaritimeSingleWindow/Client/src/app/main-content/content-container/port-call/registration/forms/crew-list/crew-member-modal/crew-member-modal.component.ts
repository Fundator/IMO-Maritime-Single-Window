import { Component, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { GenderModel, IdentityDocumentTypeModel, PersonOnBoardModel } from 'app/shared/models/';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IdentityDocumentService, PortCallFalPersonOnBoardService, ValidateDateTimeService } from 'app/shared/services/';

@Component({
  selector: 'app-crew-member-modal',
  templateUrl: './crew-member-modal.component.html',
  styleUrls: ['./crew-member-modal.component.css']
})
export class CrewMemberModalComponent implements OnInit {

  inputCrewModel: PersonOnBoardModel;
  startInputCrewModel: PersonOnBoardModel;

  crewModel: PersonOnBoardModel = new PersonOnBoardModel();
  @Output() outputCrewModel: EventEmitter<PersonOnBoardModel> = new EventEmitter();

  @ViewChild('viewModal') viewModal;
  @ViewChild('editModal') editModal;
  dirtyForm = false;

  identityDocumentTypes: IdentityDocumentTypeModel[] = [];
  genderList: GenderModel[] = [];

  booleanList: string[] = ['Yes', 'No'];
  booleanModel = {
    'Yes': true,
    'No': false
  };
  formBooleanModel = {
    'true': 'Yes',
    'false': 'No'
  };

  validDocumentDates: Boolean = true;
  issueDateAfterExpiryDateError: Boolean = false;
  expiryDateBeforeExpiryDateError: Boolean = false;

  constructor(
    private modalService: NgbModal,
    private identityDocumentService: IdentityDocumentService,
    private personOnBoardService: PortCallFalPersonOnBoardService,
    private validateDateTimeService: ValidateDateTimeService
  ) { }

  ngOnInit() {
    this.inputCrewModel = new PersonOnBoardModel();

    this.identityDocumentService.getIdentityDocumentTypes().subscribe(res => {
      this.identityDocumentTypes = res;
    });

    this.personOnBoardService.getGenderList().subscribe(res => {
      this.genderList = res;
    });
  }

  // Open modals
  openViewModal(crewModel: PersonOnBoardModel) {
    this.inputCrewModel = JSON.parse(JSON.stringify(crewModel));
    this.makeDates(this.inputCrewModel);
    this.modalService.open(this.viewModal);
  }

  openEditModal(crewModel: PersonOnBoardModel) {
    // Set model to modify
    this.inputCrewModel = JSON.parse(JSON.stringify(crewModel));
    this.makeDates(this.inputCrewModel);
    // Set model to fall back to
    this.crewModel = JSON.parse(JSON.stringify(crewModel));

    this.modalService.open(this.editModal, {
      backdrop: 'static'
    });
  }

  // Output
  editCrewMember() {
    this.outputCrewModel.emit(this.inputCrewModel);
  }

  setNationality($event) {
    this.dirtyForm = true;
    this.inputCrewModel.nationality = $event.item;
    this.inputCrewModel.nationalityId = $event.item.countryId;
  }

  setCountryOfBirth($event) {
    this.dirtyForm = true;
    this.inputCrewModel.countryOfBirth = $event.item;
    this.inputCrewModel.countryOfBirthId = $event.item.countryId;
  }

  setIssuingNation($event) {
    this.dirtyForm = true;
    this.inputCrewModel.identityDocument[0].issuingNation = $event.item;
    this.inputCrewModel.identityDocument[0].issuingNationId = $event.item.countryId;
  }

  setIdentityDocumentType($event) {
    if ($event) {
      this.inputCrewModel.identityDocument[0].identityDocumentType = $event;
      this.inputCrewModel.identityDocument[0].identityDocumentTypeId = $event.id;
    } else {
      this.resetIdentityDocumentType();
    }
  }

  setPortOfEmbarkation($event) {
    this.dirtyForm = true;
    this.inputCrewModel.portOfEmbarkation = $event;
    this.inputCrewModel.portOfEmbarkationId = $event.locationId;
  }

  setPortOfDisembarkation($event) {
    this.dirtyForm = true;
    this.inputCrewModel.portOfDisembarkation = $event;
    this.inputCrewModel.portOfDisembarkationId = $event.locationId;
  }

  setDateOfBirth($event) {
    this.dirtyForm = true;
    if ($event) {
      const date: Date = new Date($event.year, $event.month - 1, $event.day);
      this.inputCrewModel.dateOfBirth = date;
    } else {
      this.inputCrewModel.dateOfBirth = null;
    }
  }

  setIdentityDocumentIssueDate($event) {
    this.dirtyForm = true;
    let date: Date = new Date();
    if ($event) {
      date = new Date($event.year, $event.month - 1, $event.day);
    } else {
      date = null;
    }
    this.inputCrewModel.identityDocument[0].identityDocumentIssueDate = date;
    const issueDate = this.inputCrewModel.identityDocument[0].identityDocumentIssueDate;
    const expiryDate = this.inputCrewModel.identityDocument[0].identityDocumentExpiryDate;
    if (this.validateDateTimeService.checkDocumentDatesError(issueDate, expiryDate)) {
      this.issueDateAfterExpiryDateError = true;
    } else {
      this.issueDateAfterExpiryDateError = false;
      this.expiryDateBeforeExpiryDateError = false;
    }
  }

  setIdentityDocumentExpiryDate($event) {
    this.dirtyForm = true;
    let date: Date = new Date();
    if ($event) {
      date = new Date($event.year, $event.month - 1, $event.day);
    } else {
      date = null;
    }
    this.inputCrewModel.identityDocument[0].identityDocumentExpiryDate = date;
    const issueDate = this.inputCrewModel.identityDocument[0].identityDocumentIssueDate;
    const expiryDate = this.inputCrewModel.identityDocument[0].identityDocumentExpiryDate;
    if (this.validateDateTimeService.checkDocumentDatesError(issueDate, expiryDate)) {
      this.expiryDateBeforeExpiryDateError = true;
    } else {
      this.issueDateAfterExpiryDateError = false;
      this.expiryDateBeforeExpiryDateError = false;
    }
  }

  setTransit($event) {
    this.inputCrewModel.inTransit = this.booleanModel[$event];
  }

  setGender($event) {
    if ($event) {
      this.inputCrewModel.gender = $event;
      this.inputCrewModel.genderId = $event.genderId;
    } else {
      this.inputCrewModel.gender = null;
      this.inputCrewModel.genderId = null;
    }
  }

  // Resetters
  resetInputCrewModel($event: any) {
    this.resetForm();
    this.inputCrewModel = JSON.parse(JSON.stringify(this.crewModel));
  }

  resetNationality() {
    this.dirtyForm = true;
    this.inputCrewModel.nationality = null;
    this.inputCrewModel.nationalityId = null;
  }

  resetCountryOfBirth() {
    this.dirtyForm = true;
    this.inputCrewModel.countryOfBirth = null;
    this.inputCrewModel.countryOfBirthId = null;
  }

  resetIssuingNation() {
    this.dirtyForm = true;
    this.inputCrewModel.identityDocument[0].issuingNation = null;
    this.inputCrewModel.identityDocument[0].issuingNationId = null;
  }

  resetIdentityDocumentType() {
    this.inputCrewModel.identityDocument[0].identityDocumentType = null;
    this.inputCrewModel.identityDocument[0].identityDocumentTypeId = null;
  }

  resetPortOfEmbarkation() {
    this.dirtyForm = true;
    this.inputCrewModel.portOfEmbarkation = null;
    this.inputCrewModel.portOfEmbarkationId = null;
  }

  resetPortOfDisembarkation() {
    this.dirtyForm = true;
    this.inputCrewModel.portOfDisembarkation = null;
    this.inputCrewModel.portOfDisembarkationId = null;
  }

  resetForm() {
    this.dirtyForm = false;
  }

  // Helper methods
  getNgbDateFormat(date) {
    if (date != null) {
      const newDate = new Date(date);
      return {
        year: newDate.getFullYear(),
        month: newDate.getMonth() + 1,
        day: newDate.getDate()
      };
    } else {
      return null;
    }
  }

  getDateFormatFromNgbDate(date) {
    if (date) {
      const newDate = new Date(date.year + '-' + date.month + '-' + date.day);
      return newDate;
    }
    return null;
  }

  getDisplayDateFormat(date) {
    if (date) {
      const dateString = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
      return dateString;
    } else {
      return null;
    }
  }

  makeDates(crewMember: PersonOnBoardModel) {
    crewMember.dateOfBirth = crewMember.dateOfBirth != null ? new Date(crewMember.dateOfBirth) : null;
        crewMember.identityDocument.forEach(identityDocument => {
          identityDocument.identityDocumentIssueDate = identityDocument.identityDocumentIssueDate != null ? new Date(identityDocument.identityDocumentIssueDate) : null;
          identityDocument.identityDocumentExpiryDate = identityDocument.identityDocumentExpiryDate != null ? new Date(identityDocument.identityDocumentExpiryDate) : null;
        });
    return crewMember;
  }

}
