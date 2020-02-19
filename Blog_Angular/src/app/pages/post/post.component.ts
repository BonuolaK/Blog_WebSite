import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CategoryForm } from '../../models/category-form';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PostForm } from '../../models/post-form';
import { PostService } from '../../services/post.service';
import { DateValidator } from '../../Utils/DateValidator';
import { Category } from '../../models/category';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @ViewChild('focusInput') focusInput: ElementRef; 
  postId: number = 0;
  postForm: FormGroup;
  postObj: PostForm;
  categories: Category[];
  alertClass: string;
  alertMessage: string;
  action: string;


  constructor(private route: ActivatedRoute,
    private router: Router,
    private categorySvc: CategoryService,
    private postSvc: PostService) {
    let id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.postId = Number(id);
      this.action = "Update";
    }
    else {
      this.action = "Add";
    }

  }

  ngOnInit() {

    this.focusInput.nativeElement.focus();
    this.categorySvc.getAll().subscribe(entries => {
      this.categories = entries;
    }, err => { console.log(err) });

    this.postForm = new FormGroup({
      'title': new FormControl('', [Validators.required]),
      'categoryId': new FormControl('', [Validators.required]),
      'publicationDate': new FormControl('', [Validators.required, DateValidator()]),
      'content': new FormControl('', [Validators.required, Validators.minLength(10)])
    });

    if (this.postId > 0) {
      this.postSvc.get(this.postId).subscribe(x => {
        this.postForm.patchValue({
          title: x.title,
          categoryId : x.categoryId,
          publicationDate: x.publicationDateFormat,
          content: x.content
        });
      }, err => { console.log(err) });

    }
  }

  redirectToHome() {
    setTimeout(() => {
      this.router.navigate(['/home']);
    },
      5000);
  }

  clearAlert() {
    this.alertMessage = null;
  }

  showAlert(hasError: boolean, message: string) {
    if (hasError)
      this.alertClass = "danger";
    else
      this.alertClass = "success";

    this.alertMessage = message;
  }

  handleError(err) {
    this.showAlert(true, err.error.errors[0]);
  }

  save() {
    this.clearAlert();
    this.postObj = {
      id: this.postId,
      title: this.postForm.get('title').value,
      categoryId: Number(this.postForm.get('categoryId').value),
      content: this.postForm.get('content').value,
      publicationDate: this.postForm.get('publicationDate').value
    };


    if (this.postId == 0) {
      this.postSvc.create(this.postObj).subscribe(x => {
        // success message
        this.showAlert(false, "Successfully Created. Please wait to be redirected....");
        this.redirectToHome();
      }, err => {
          this.handleError(err);
      });
    }
    else {
      this.postObj.id = 20;
      this.postSvc.update(this.postObj).subscribe(x => {
        // success message
        this.showAlert(false, "Successfully Updated. Please wait to be redirected....");

        this.redirectToHome();
      }, err => { this.handleError(err); });
    }
  }

}
