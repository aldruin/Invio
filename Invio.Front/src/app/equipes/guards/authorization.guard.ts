import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { map, Observable } from "rxjs";
import { SharedService } from "../../shared/shared.service";
import { User } from "../../shared/models/user";
import { AccountService } from "../../account/account.service";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard {
  constructor(private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.user$.pipe(
      map((user: User | null) => {
        if (user) {
          return true;
        } else {
          this.sharedService.showNotification(false, 'Área restrita!', 'Faça login para acessar')
          this.router.navigate(['account/login'], { queryParams: { returnUrl: state.url } });
          return false;
        }
      })
    );
  }
}