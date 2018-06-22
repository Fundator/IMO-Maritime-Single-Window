import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { SharedModule } from 'app/shared/shared.module';
import { RegisterLocationComponent } from './location/register-location/register-location.component';
import { ViewLocationInfoComponent } from './location/view-location-info/view-location-info.component';
import { RegisterOrganizationComponent } from './organization/register-organization/register-organization.component';
import { ViewOrganizationInfoComponent } from './organization/view-organization-info/view-organization-info.component';
import { RegisterShipComponent } from './ship/register-ship/register-ship.component';
import { SearchShipFlagCodeComponent } from './ship/search-ship-flag-code/search-ship-flag-code.component';
import { ViewShipInfoComponent } from './ship/view-ship-info/view-ship-info.component';
import { RegisterUserComponent } from './user/register-user/register-user.component';
import { LocationService } from '../../../shared/services/location.service';
import { OrganizationService } from '../../../shared/services/organization.service';
import { ShipService } from '../../../shared/services/ship.service';
import { ContactService } from '../../../shared/services/contact.service';

@NgModule({
  imports: [
    NgbModule,
    CommonModule,
    FormsModule,
    HttpModule,
    NgSelectModule,
    SharedModule
  ],
  declarations: [
    RegisterUserComponent,
    RegisterShipComponent,
    SearchShipFlagCodeComponent,
    ViewShipInfoComponent,
    RegisterOrganizationComponent,
    ViewOrganizationInfoComponent,
    RegisterLocationComponent,
    ViewLocationInfoComponent,
  ],
  exports: [
    RegisterUserComponent,
    ViewShipInfoComponent,
    RegisterShipComponent,
    RegisterLocationComponent,
    RegisterOrganizationComponent,
    ViewOrganizationInfoComponent,
    ViewLocationInfoComponent,
  ],
  providers: [
    LocationService,
    OrganizationService,
    ShipService,
    ContactService
  ]
})
export class BasisDataModule { }
