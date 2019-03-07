import { Component, OnInit } from '@angular/core';
import {DataService} from '../data.service';

import {FormBuilder,FormGroup,Validators} from '@angular/forms';
import { strictEqual } from 'assert';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private data:DataService,private formBuilder: FormBuilder) { }
  users:Object;
  userMore:Object;

  nameFirst:string;
  name:string;
  surname:string;
  gender:string;
  work:string;
  sendString:string;
  
  insertForm: FormGroup;

  h1Style :boolean = false;
  submitted = false;
  success = false;

  ngOnInit() {
    this.data.getUsers().subscribe(data=>{
      this.users = data; 
      console.log(this.success);
    });

    this.insertForm = this.formBuilder.group({
      nameFirst:['', Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required]     
    });
  }
  onSubmit() {
    this.submitted = true;

    if (this.insertForm.invalid) {
        return;
    }
    this.nameFirst = this.insertForm.controls.nameFirst.value;
    this.name = this.insertForm.controls.name.value;
    this.surname = this.insertForm.controls.surname.value;

    this.success = true;
}
    getMore()
    {
      this.data.getUsersDetailed().subscribe(data=>{
        this.userMore = data; 
        console.log(this.userMore);
      });
    }
    getInsert()
    {
      this.data.insertUserDetails(this.name,this.surname,this.gender,this.work).subscribe(data=>{
        this.userMore = data;
      });
    }
    updateUser()
    {
      this.sendString ="?nameFirst"+this.nameFirst+"&name="+this.name+"&surname="+this.surname;
      this.data.updateUserDetails(this.sendString).subscribe(data=>{
        this.users = data;
      });
    }
    firstClick()
    {
      this.h1Style = !this.h1Style

      this.data.firstClick();
    }
}
