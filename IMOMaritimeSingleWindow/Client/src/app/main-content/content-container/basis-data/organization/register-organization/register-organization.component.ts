import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationModalComponent } from 'app/shared/components/confirmation-modal/confirmation-modal.component';
import { CONTENT_NAMES } from 'app/shared/constants/content-names';
import { OrganizationModel } from 'app/shared/models/organization-model';
import { ContentService } from 'app/shared/services/content.service';
import { OrganizationService } from 'app/shared/services/organization.service';

const RESULT_SUCCESS = 'Organization was successfully saved to the database.';
const RESULT_FAILURE = 'There was a problem when trying to save the organization to the database. Please try again later.';

@Component({
  selector: 'app-register-organization',
  templateUrl: './register-organization.component.html',
  styleUrls: ['./register-organization.component.css'],
  providers: [OrganizationModel]
})
export class RegisterOrganizationComponent implements OnInit {
  newOrganization = false;
  organizationHeader: string;
  confirmHeader: string;
  confirmButtonTitle: string;
  organizationTypeList: any[];
  organizationTypeSelected: boolean;
  selectedOrganizationType: any;
  organizationTypeDropdownString = 'Select organization type';

  constructor(public organizationModel: OrganizationModel, private organizationService: OrganizationService,
    private contentService: ContentService, private modalService: NgbModal) { }

  ngOnInit() {
    this.organizationService.organizationData$.subscribe(
      data => {
        if (data) {
          this.organizationHeader = 'Edit Organization';
          this.confirmHeader = 'Confirm Organization Changes';
          this.confirmButtonTitle = 'Apply Changes';
          this.organizationModel = data;
          this.selectedOrganizationType = this.organizationModel.organizationType;
          this.organizationTypeSelected = true;
        } else if (!this.newOrganization) {
          this.newOrganization = true;
          this.organizationHeader = 'Register New Organization';
          this.confirmHeader = 'Confirm Organization Registration';
          this.confirmButtonTitle = 'Register Organization';
        }
      }
    );
    this.organizationService.getOrganizationTypes().subscribe(
      organizationTypesData => {
        this.organizationTypeList = organizationTypesData;
        // Temporary until we add more organization types (certificate issuer)
        if (this.newOrganization) {
          this.selectedOrganizationType = this.organizationTypeList.find(type => type.name === 'Authority');
          this.organizationTypeSelected = true;
        }
      }
    );
  }

  registerOrganization() {
    if (this.newOrganization) {
      this.organizationService.registerOrganization(this.organizationModel).subscribe(
        result => {
          this.openConfirmationModal(ConfirmationModalComponent.TYPE_SUCCESS, RESULT_SUCCESS);
        }, error => {
          console.log(error);
          this.openConfirmationModal(ConfirmationModalComponent.TYPE_FAILURE, RESULT_FAILURE);
        }
      );
    } else {
      this.organizationService.updateOrganization(this.organizationModel).subscribe(
        result => {
          this.openConfirmationModal(ConfirmationModalComponent.TYPE_SUCCESS, RESULT_SUCCESS);
        }, error => {
          console.log(error);
          this.openConfirmationModal(ConfirmationModalComponent.TYPE_FAILURE, RESULT_FAILURE);
        }
      );
    }
  }

  selectOrganizationType(organizationType: any) {
    this.organizationModel.organizationTypeId = organizationType.organizationTypeId;
    this.organizationTypeDropdownString = organizationType.name;
    this.selectedOrganizationType = organizationType;
    this.organizationTypeSelected = true;
  }

  private goBack() {
    this.contentService.setContent(CONTENT_NAMES.VIEW_ORGANIZATIONS);
  }

  private openConfirmationModal(modalType: string, bodyText: string) {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.modalType = modalType;
    modalRef.componentInstance.bodyText = bodyText;
    modalRef.result.then(
      result => {
        if (modalType !== ConfirmationModalComponent.TYPE_FAILURE) { this.goBack(); }
      },
      reason => {
        if (modalType !== ConfirmationModalComponent.TYPE_FAILURE) { this.goBack(); }
      }
    );
  }

}
