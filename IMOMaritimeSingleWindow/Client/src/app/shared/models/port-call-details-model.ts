export class PortCallDetailsModel {
    portCallDetailsId: number;
    portCallId: number;
    cargoGrossWeight: number;
    cargoGrossGrossWeight: number;
    numberOfCrew: number;
    numberOfPassengers: number;
    actualDraught: number;
    airDraught: number;
    reportingHazmat: boolean;
    reportingBunkers: boolean;
    reportingCargo: boolean;
    reportingShipStores: boolean;
    reportingCrew: boolean;
    reportingPax: boolean;
    reportingWaste: boolean;
}