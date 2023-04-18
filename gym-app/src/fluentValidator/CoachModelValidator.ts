import { Validator } from "fluentvalidation-ts";
import { ICoachModel } from "../Interfaces/ICoachModel";

export class CoachModelValidator extends Validator<ICoachModel> {
    /**
     *
     */
    constructor() {
        super();
        this.ruleFor("firstName")
        .notEmpty()
        .withMessage("Cannot be emty.")
        .minLength(2)
        .withMessage("Lenght less than 2.")
        
        this.ruleFor("lastName")
        .notEmpty()
        .withMessage("Cannot be emty.")
        .minLength(2)
        .withMessage("lengh less than 2.");
        
        this.ruleFor("description")
        .notEmpty()
        .withMessage("Cannot be emty")
        .minLength(5)
        .withMessage("lengh less than 5");
    }
}