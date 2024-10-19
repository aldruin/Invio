import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class EquipesService {

  constructor(private http: HttpClient) { }

  getEquipes
    () {
    return this.http.get(`${environment.appUrl}/api/equipe/obter-todos`);
  }
}
