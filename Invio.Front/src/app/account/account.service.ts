import { environment } from './../../environments/environment.development';
import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../shared/models/register';
import { Login } from '../shared/models/login';
import { User } from '../shared/models/user';
import { map, of, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSource = new ReplaySubject< User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient) { }

  refreshUser(jwt: string | null){
    if( jwt === null) {
      this.userSource.next(null);
      return of(undefined);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http.get<User>(`${environment.appUrl}/api/Autenticacao/refresh-user-token`)
  }

  login(model:Login){
    return this.http.post<User>(`${environment.appUrl}/api/autenticacao/login`,model).pipe(
      map((user: User) => {
        if (user) {
          this.setUser(user);
        }
      })
    );
  }

  register(model: Register){
    return this.http.post(`${environment.appUrl}/api/Usuario/Criar`, model);
  }

  getJWT(){
    const key = localStorage.getItem(environment.userKey);
    if (key){
      const user: User = JSON.parse(key);
      return user.jwt;
    } else {
      return null;
    }
  }

  private setUser(user: User) {
    localStorage.setItem(environment.userKey, JSON.stringify(user));
    this.userSource.next(user);

  }
}
