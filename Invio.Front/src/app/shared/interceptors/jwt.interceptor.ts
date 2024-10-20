import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, take } from "rxjs";
import { AccountService } from "../../account/account.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.user$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          // clone from the coming request and add auth header to that
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${user.jwtToken}`
            }
          });
        }
      }
    })
    return next.handle(request);
  }
}