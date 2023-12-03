import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { throwError as observableThrowError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }


  private handleError(error: any) {
    return observableThrowError(error.error || 'Server error');
  }

  httpGet<T>(url: string) {
    return this.http.get<T>(this.apiUrl + url).pipe(
      tap((res: any) => {
        res

        console.log(url);

        console.log(res);
      }
      ),
      catchError(this.handleError)
    );
  }

  httpPost<T>(url: string, model: any = null) {
    return this.http.post<T>(this.apiUrl + url, model).pipe(
      tap((res: any) => {
        res
        console.log(url);
        console.log(model);

        console.log(res);
      }
      ),
      catchError(this.handleError)
    );
  }

  httpPut<T>(url: string, model: any = null) {
    return this.http.put<T>(this.apiUrl + url, model).pipe(
      tap((res: any) => {
        res
        console.log(url);
        console.log(model);

        console.log(res);
      }
      ),
      catchError(this.handleError)
    );
  }

  httpDelete(url: string) {
    return this.http.delete(this.apiUrl + url).pipe(
      tap((res: any) => {
        res
        
        console.log(url);

        console.log(res);
      }
      ),
      catchError(this.handleError)
    );
  }
}

