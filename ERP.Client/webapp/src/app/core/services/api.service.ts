import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private _http = inject(HttpClient);

  get = <T>(endpoint: string, options?: object) => this._http.get<T>(this.generateURL(endpoint), options);
  post = <T>(endpoint: string, body: T, options?: object) => this._http.post<string>(this.generateURL(endpoint), body, options);
  put = <T>(endpoint: string, body: T, options?: object) => this._http.put(this.generateURL(endpoint), body, options);
  patch = <T>(endpoint: string, body: T, options?: object) => this._http.patch(this.generateURL(endpoint), body, options);
  delete = (endpoint: string, options?: object) => this._http.delete(this.generateURL(endpoint), options);

  private generateURL = (endpoint: string) => `http://localhost:5143/api` + endpoint;
}
