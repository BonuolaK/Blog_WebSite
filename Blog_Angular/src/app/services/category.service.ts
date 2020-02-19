import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CategoryForm } from '../models/category-form';
import { Injectable } from '@angular/core';
import { Category } from '../models/category';

@Injectable()
export class CategoryService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<any>(`${environment.apiBaseUrl}/categories`);
  }

  get(id: number): Observable<Category> {
    return this.http.get<any>(`${environment.apiBaseUrl}/categories/${id}`);
  }

  getPosts(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiBaseUrl}/categories/${id}/posts`);
  }

  create(model: CategoryForm): Observable<any> {
    return this.http.post<any>(`${environment.apiBaseUrl}/categories`, model);
  }

  update(model: CategoryForm): Observable<any> {
    return this.http.put<any>(`${environment.apiBaseUrl}/categories`, model);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiBaseUrl}/categories/${id}`);
  }


}
