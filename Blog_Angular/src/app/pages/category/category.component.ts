import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CategoryForm } from '../../models/category-form';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  @ViewChild('focusInput') focusInput: ElementRef; 
  categoryId: number = 0;
  categoryForm: FormGroup;
  category: CategoryForm;
  alertClass: string;
  alertMessage: string;
  action: string;


  constructor(private route: ActivatedRoute,
    private router: Router,
    private categorySvc: CategoryService) {
    let id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.categoryId = Number(id);
      this.action = "Update";
    }
    else {
      this.action = "Add";
    }
      
  }

  ngOnInit() {
    this.focusInput.nativeElement.focus();
    this.categoryForm = new FormGroup({
      'name': new FormControl('', [Validators.required])
    });

    if (this.categoryId > 0) {
      this.categorySvc.get(this.categoryId).subscribe(x => {
        this.categoryForm.patchValue({
          name: x.name
        });
      }, err => { console.log(err) });

    }
  }


  handleError(err) {
    this.showAlert(true, err.error.errors[0]);
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

  save() {
    this.clearAlert();
    this.category = { id: this.categoryId, name: this.categoryForm.get('name').value };

    
    if (this.categoryId == 0) {
      this.categorySvc.create(this.category).subscribe(x => {
        // success message
        this.showAlert(false, "Successfully Created. Please wait to be redirected....");
        this.redirectToHome();
      }, err => { this.handleError(err) });
    }
    else {
      this.categorySvc.update(this.category).subscribe(x => {
        // success message
        this.showAlert(false, "Successfully Updated. Please wait to be redirected....");

        this.redirectToHome();
      }, err => { this.handleError(err); });
    }
  }

}
