import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { FormMetaData } from 'app/shared/interfaces/form-meta-data.interface';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { ShipStoresModel } from '../models/ship-stores-model';

@Injectable()
export class FalShipStoresService {

  private shipStoresUrl = 'api/falShipStores';
  private portCallUrl = 'api/portCall';
  private shipStoresListString = 'list';
  private shipStoresString = 'falShipStores';
  private measurementTypeUrl = 'api/measurementType';

  constructor(private http: Http) { }

  private shipStoresInformationSource = new BehaviorSubject<ShipStoresModel[]>(null);
  shipStoresList$ = this.shipStoresInformationSource.asObservable();

  private shipStoresInformationMeta = new BehaviorSubject<any>({ valid: true });
  shipStoresInformationMeta$ = this.shipStoresInformationMeta.asObservable();

  private dataIsPristine = new BehaviorSubject<boolean>(true);
  dataIsPristine$ = this.dataIsPristine.asObservable();

  private sequenceNumberSource = new BehaviorSubject<number>(1);
  sequenceNumber$ = this.sequenceNumberSource.asObservable();

  private isCheckedInProgressBar = new BehaviorSubject<boolean>(false);
  isCheckedInProgressBar$ = this.isCheckedInProgressBar.asObservable();

  // API calls
  // Get ship stores object by its primary key ID
  getShipStoresById(shipStoresId: number) {
    const uri = [this.shipStoresUrl, shipStoresId].join('/');
    return this.http.get(uri).map(res => res.json());
  }

  // Save ship stores list to database
  saveShipStores(shipStoresList: any[], portCallId: number) {
    console.log('Saving ship stores...');
    console.log(shipStoresList);
    const uri = [this.shipStoresUrl, portCallId, this.shipStoresListString].join('/');
    return this.http.put(uri, shipStoresList);
  }

  // Get all ship stores for a given port call
  getShipStoresByPortCallId(portCallId: number) {
    let uri = [this.portCallUrl, portCallId].join('/');
    uri = [uri, this.shipStoresString].join('/');

    return this.http.get(uri).map(res => res.json());
  }
  // Get list of all measurement types
  getMeasurementTypeList() {
    const uri = this.measurementTypeUrl;
    return this.http.get(uri).map(res => res.json());
  }

  /************************
   *
   *  SETTERS AND DELETE
   *
   ************************/

  // Update shipStoresInformationData
  setShipStoresInformationData(data) {
    console.log(data);
    this.shipStoresInformationSource.next(data);
  }

  // Update setShipStoresInformationMeta
  setShipStoresInformationMeta(metaData: FormMetaData) {
    this.shipStoresInformationMeta.next(metaData);
  }

  setDataIsPristine(isPristine: boolean) {
    this.dataIsPristine.next(isPristine);
  }

  setCheckedInProgressBar(checked: boolean) {
    this.isCheckedInProgressBar.next(checked);
  }

  /******************
   *
   *  HELP METHODS
   *
   ******************/

  setSequenceNumbers(list) {
    let tempSequenceNumber = 1;
    list.forEach(item => {
      item.sequenceNumber = tempSequenceNumber;
      tempSequenceNumber++;
    });
    return list;
  }

  formatShipStores(shipStoresList: ShipStoresModel[]): ShipStoresModel[] {
    let formattedList: ShipStoresModel[] = [];
    if (shipStoresList && shipStoresList.length > 0) {
      formattedList = shipStoresList.map(
        item => {
          const formattedShipStore = new ShipStoresModel();
          formattedShipStore.portCallId = item.portCallId;
          formattedShipStore.sequenceNumber = item.sequenceNumber;
          formattedShipStore.articleName = item.articleName;
          formattedShipStore.articleCode = item.articleCode;
          formattedShipStore.quantity = item.quantity;
          formattedShipStore.locationOnBoardCode = item.locationOnBoardCode;
          formattedShipStore.locationOnBoard = item.locationOnBoard;
          formattedShipStore.measurementTypeId = item.measurementTypeId;
          formattedShipStore.articleName = item.articleName;
          return formattedShipStore;
        }
      );
    }
    return formattedList;
  }

}
