import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { NotificacionService } from '../services/notificacion.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private notificacionService: NotificacionService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        const respuesta = error.error;
        let mensaje = 'Ocurrió un error inesperado.';

        if (respuesta?.detalles?.length) {
          mensaje = respuesta.detalles[0];
        } else if (respuesta?.mensaje) {
          mensaje = respuesta.mensaje;
        } else if (error.message) {
          mensaje = error.message;
        }

        this.notificacionService.error('Error', mensaje);

        return throwError(() => error);
      })
    );
  }
}