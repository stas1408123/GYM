export interface IVisitorModel {
    id:number,
    firstName:string,
    lastNAme:string,
    couches:{
        couchId : number,
        coachFirstName : string,
        coachLastName : string,

    }[]

}
