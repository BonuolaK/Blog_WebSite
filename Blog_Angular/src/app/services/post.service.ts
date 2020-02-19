import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PostForm } from '../models/post-form';
import { Post } from '../models/post';

@Injectable()
export class PostService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Post[]> {
    return this.http.get<any>(`${environment.apiBaseUrl}/posts`);
  }

  get(id: number): Observable<Post> {
    return this.http.get<any>(`${environment.apiBaseUrl}/posts/${id}`);
  }

  
  create(model: PostForm): Observable<any> {
    return this.http.post<any>(`${environment.apiBaseUrl}/posts`, model);
  }

  update(model: PostForm): Observable<any> {
    return this.http.put<any>(`${environment.apiBaseUrl}/posts`, model);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiBaseUrl}/posts/${id}`);
  }


}
