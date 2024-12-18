import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IPropertyBase } from '../Model/Ipropertybase';
import { IProperty } from '../Model/Iproperty';
import { Property } from '../Model/Property';
import { environment } from '../../environments/environment';
import { KeyValue } from '@angular/common';
import { IKeyValue } from '../Model/ikey-value';


@Injectable({
  providedIn: 'root'
})
export class HousingService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getAllCities():Observable<string[]>{
    return this.http.get<string[]>('https://localhost:7042/api/City');
  }

  getProperty(id: number) {
    return this.http.get<Property>(this.baseUrl+"/Property/detail/"+ id.toString());
  }

  getAllProperties(SellRent?: number): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + '/Property/list/'+ SellRent.toString());
  }

  getPropertyType(): Observable<IKeyValue[]> {
    return this.http.get<IKeyValue[]>(this.baseUrl + '/PropertyType/list');
  }

  getFurnishingType(): Observable<IKeyValue[]> {
    return this.http.get<IKeyValue[]>(this.baseUrl + '/FurnishingType/list');
  }

  addProperty(property: Property) {
    const httpOptions = {
      headers: new HttpHeaders({
          Authorization: 'Bearer '+ localStorage.getItem('token')
      })
    };
  return this.http.post(this.baseUrl + '/Property/add', property, httpOptions);
  }

  newPropID() {
    if (localStorage.getItem('PID')) {
      localStorage.setItem('PID', String(+localStorage.getItem('PID') + 1));
      return +localStorage.getItem('PID');
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }

  getPropertyAge(dateofEstablishment: string): string
    {
        const today = new Date();
        const estDate = new Date(dateofEstablishment);
        let age = today.getFullYear() - estDate.getFullYear();
        const m = today.getMonth() - estDate.getMonth();

        // Current month smaller than establishment month or
        // Same month but current date smaller than establishment date
        if (m < 0 || (m === 0 && today.getDate() < estDate.getDate())) {
            age --;
        }

        // Establshment date is future date
        if(today < estDate) {
            return '0';
        }

        // Age is less than a year
        if(age === 0) {
            return 'Less than a year';
        }

        return age.toString();
    }

    setPrimaryPhoto(propertyId: number, propertyPhotoId: string) {
      const httpOptions = {
          headers: new HttpHeaders({
              Authorization: 'Bearer '+ localStorage.getItem('token')
          })
      };
      return this.http.post(this.baseUrl + '/Property/set-primary-photo/'+String(propertyId)
          +'/'+propertyPhotoId, {}, httpOptions);
  }

  deletePhoto(propertyId: number, propertyPhotoId: string) {
      const httpOptions = {
          headers: new HttpHeaders({
              Authorization: 'Bearer '+ localStorage.getItem('token')
          })
      };
      return this.http.delete(this.baseUrl + '/Property/delete-photo/'
          +String(propertyId)+'/'+propertyPhotoId, httpOptions);
  }
}