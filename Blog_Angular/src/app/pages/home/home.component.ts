import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { PostService } from '../../services/post.service';
import { Category } from '../../models/category';
import { Post } from '../../models/post';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  categories: Category[];
  posts: Post[];
  hasFilter: boolean;
  selectedCategory: string

  constructor(private categorySvc: CategoryService,
    private postSvc: PostService
      ) { }

  ngOnInit() {

    this.categorySvc.getAll().subscribe(entries => {
      this.categories = entries;
    }, err => { console.log(err) });

    this.getPosts();

  }

  getPosts() {
    this.postSvc.getAll().subscribe(entries => {
      this.posts = entries;
    }, err => { console.log(err) });
  }

  clearFilters() {
    this.getPosts();
    this.hasFilter = false;
    this.selectedCategory = null;
  }

  filterPosts(id: number, category:string) {
    this.categorySvc.getPosts(id).subscribe(entries => {
      this.posts = entries;
      this.hasFilter = true;
      this.selectedCategory = category;
    },
      err => { console.log(err) });
  }

}
