import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { AccountModelResponse } from './account.model';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    accountModel: AccountModelResponse = new AccountModelResponse();
    constructor(private _httpService: Http) {  }
    title: string = "testing CI";
    accountName: string = '';
    ngOnInit() {
    };
    refresh() {
        this.accountModel.setModel({});
    };
    SearchByName() {
        this._httpService.get('/api/search/' + this.accountName).subscribe(values => {
            this.accountModel.setModel(values);
        });
    }
}
