import {CountryModel, IdentityDocumentTypeModel } from './';

export class IdentityDocumentModel {
    identityDocumentId: number;
    identityDocumentTypeId: number;
    issuingNationId: number;
    visaOrResidencePermitNumber: number;
    identityDocumentIssueDate: any;
    identityDocumentExpiryDate: any;
    identityDocumentNumber: number;
    personOnBoardId: number;

    identityDocumentType: IdentityDocumentTypeModel;
    issuingNation: any;
}
