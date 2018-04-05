import { Component, OnInit } from '@angular/core';
import { PortCallService } from '../../../../shared/services/port-call.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  portCallShipInfo: any[] = [
    {description : "Ship Name:", data: null},
    {description : "Call Sign:", data: null},
    {description : "IMO no:", data: null},
    {description : "Gross Tonnage:", data: null},
    {description : "Length:", data: null},
    {description : "Ship Type:", data: null}
  ];

  portCallLocationInfo: any[] = [
    {description : "Location:", data: null},
    {description : "ETA:", data: null},
    {description : "ETD:", data: null}
  ];

  constructor(private portCallService: PortCallService) { }

  ngOnInit() {
    this.portCallService.shipData$.subscribe(
      data => {
        if (data != null) {
          this.portCallShipInfo.find(p => p.description == "Ship Name:").data = data.ship.name;
          this.portCallShipInfo.find(p => p.description == "Call Sign:").data = data.ship.callSign;
          this.portCallShipInfo.find(p => p.description == "IMO no:").data = data.ship.imoNo;
          this.portCallShipInfo.find(p => p.description == "Gross Tonnage:").data = data.ship.grossTonnage;
          this.portCallShipInfo.find(p => p.description == "Length:").data = data.ship.length;
          // this.portCallShipInfo.find(p => p.description == "Ship Type:").data = data.ship.type;
        }
      }
    );

  }

}
