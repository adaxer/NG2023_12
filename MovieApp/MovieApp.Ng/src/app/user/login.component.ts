import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from './user.service';
import { CanBeDirty } from '../models/can-be-dirty';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styles: ``
})
export class LoginComponent implements OnInit, CanBeDirty {
  constructor(private fb: FormBuilder, private userService: UserService) { }
  form!: FormGroup;
  get isDirty(): boolean
  {
    if(this.form.invalid && this.form.touched) {
      return true;
    }
    return false;
  }

  email?: string = "Test@test.de";
  password?: string;
  isRegistered: boolean = false;

  get canRegister(): boolean {
    return this.form.valid;
  }

  get canLogin(): boolean {
    return this.form.valid && this.isRegistered;
  }

  ngOnInit() {
    // https://stackoverflow.com/questions/70106472/property-fname-comes-from-an-index-signature-so-it-must-be-accessed-with-fn
    this.form = this.fb.group({
      email: [this.email, Validators.required],
      password: [this.password, Validators.required]
    });
  }

  checkForm(): boolean {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.email = this.form.value.email;
      this.password = this.form.value.password;
      console.log('Form is Submitted!', this.form.value);
    } else {
      console.log('Form is invalid!');
    }
    return this.form.valid;
  }

  tryRegister() {
    if (!this.checkForm()) {
      return;
    }
    this.userService.register(this.email!, this.password!).subscribe(b => this.isRegistered = b);
  }

  tryLogin() {
    if (!this.checkForm()) {
      return;
    }
    this.userService.login(this.email!, this.password!).subscribe(b => {
      console.log("Successful login? ", b);
    });
  }
}
