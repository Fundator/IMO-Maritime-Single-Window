import { GenderModel, IdentityDocumentModel, PersonOnBoardTypeModel, PortCallModel } from 'app/shared/models/';

export class PersonOnBoardModel {
    personOnBoardId: number;
    givenName: string;
    familyName: string;
    dateOfBirth: any;
    placeOfBirth: string;
    occupationName: string;
    occupationCode: string;
    roleCode: string;
    inTransit: boolean; // true/false indicator of whether the referenced person is in transit to a foreign country
    rankName: string;
    rankCode: string;
    sequenceNumber: number;
    isPax: boolean;

    // ids
    countryOfBirthId: number; // country id
    nationalityId: number;  // country id
    personOnBoardTypeId: number; // person on board type id
    genderId: number; // gender id
    portCallId: number; // port call id
    portOfEmbarkationId: number;
    portOfDisembarkationId: number;
    // models
    countryOfBirth: any;
    nationality: any;
    personOnBoardType: PersonOnBoardTypeModel;
    gender: GenderModel;
    portCall: PortCallModel;
    portOfEmbarkation: any;
    portOfDisembarkation: any;
    identityDocument: IdentityDocumentModel[] = [];

}
