export class AccountModelResponse {
    public accounts: AccountModel[] = [];

    constructor() { }

    setModel(data) {
        let body = data.json() || {};

        this.accounts = [];

        for (var i = 0; i < body.length; i++) {
            var account = new AccountModel(body[i]);
            this.accounts.push(account);
        }        
    }
}



export class AccountModel {
    public Id: string = "";
    public Number: number = 0;
    public Name: string = "";
    public Balance: number = 0.0;


    constructor(data) {
        this.Id = data.id;
        this.Number = data.number;
        this.Name = data.name;
        this.Balance = data.balance;
    }

}
