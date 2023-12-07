import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styles: ``
})
export class LoginComponent implements OnInit {
  constructor(private fb: FormBuilder) { }
  form!: FormGroup;
  isDirty = false;

  email?: string;
  password?: string;

  ngOnInit() {
    // https://stackoverflow.com/questions/70106472/property-fname-comes-from-an-index-signature-so-it-must-be-accessed-with-fn
    this.form = this.fb.group({
      email: [this.email, Validators.required],
      password: [this.password, Validators.required]
    });
  }

  onSubmit() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.email = this.form.value.email;
      this.password = this.form.value.password;
      console.log('Form is Submitted!', this.form.value);
      this.isDirty = true;
    } else {
      console.log('Form is invalid!');
    }
  }
}
