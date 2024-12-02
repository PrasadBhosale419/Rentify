import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './Property/nav-bar/nav-bar.component';
import { PropertyListComponent } from './Property/property-list/property-list.component';
import { PropertyCardComponent } from './Property/property-card/property-card.component';
import {provideHttpClient } from '@angular/common/http';
import { HousingService } from './Services/housing.service';
import { AddPropertyComponent } from './Property/add-property/add-property.component';
import { RouterModule, Routes } from '@angular/router';
import { RentPropertyComponent } from './Property/rent-property/rent-property.component';
import { PropertyDetailComponent } from './Property/property-detail/property-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserLoginComponent } from './User/user-login/user-login.component';
import { UserRegisterComponent } from './User/user-register/user-register.component';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import {ButtonsModule} from 'ngx-bootstrap/buttons';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker'
import { PropertyDetailResolverService } from './Property/property-detail/property-detail-resolver.service';
import { FilterPipe } from './Pipes/filter.pipe';
import { SortPipe } from './Pipes/sort.pipe';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptorService } from './Services/httperror-interceptor.interceptor';
import { DatePipe } from '@angular/common';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoEditorComponent } from './Property/photo-editor/photo-editor.component';

const Routes : Routes=[
  {path:'', component:PropertyListComponent},
  {path:'add-property', component:AddPropertyComponent},
  {path:'rent-property', component:PropertyListComponent},
  {path:'property-detail/:id', component:PropertyDetailComponent, resolve: {prp: PropertyDetailResolverService}},
  {path:'user/user-login', component:UserLoginComponent},
  {path:'user/user-register', component:UserRegisterComponent},
  {path:'**', component:PropertyListComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    PropertyListComponent,
    PropertyCardComponent,
    AddPropertyComponent,
    RentPropertyComponent,
    PropertyDetailComponent,
    UserLoginComponent,
    UserRegisterComponent,
    FilterPipe,
    SortPipe,
    PhotoEditorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(Routes),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule,
    TabsModule,
    ButtonsModule,
    BsDatepickerModule,
    NgxGalleryModule,
    FileUploadModule
  ],
  providers: [HousingService, provideHttpClient(),
    {provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptorService,
    multi: true},
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
