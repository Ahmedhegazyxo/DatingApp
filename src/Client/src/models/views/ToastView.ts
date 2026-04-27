import { Severity } from "../enums/severity";

export class ToastView 
{
    public title : string = '';
    public description : string = '';
    public duration : number = 3000;
    public severity : Severity = Severity.Info;
    public callback : ()=> void = ()=>{};
    
 }
