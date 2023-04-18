import { ErrorMessage } from "../ErrorMessage";
import { ICoachModel } from "../../Interfaces/ICoachModel";
import { useState } from "react";
import { CoachModelValidator } from "../../fluentValidator/CoachModelValidator";

import {
  DESCRIPTION_INPUT,
  FIRST_NAME_INPUT,
  LAST_NAME_INPUT,
} from "../../constants/constantsForInput";
import { ValidationErrors } from "fluentvalidation-ts/dist/ValidationErrors";

interface IUpdateCouchProps {
  coachModel: ICoachModel;
  updateCouch: (couch: ICoachModel) => void;
  closeModalWindow: () => void;
}

export function UpdateCoach({
  coachModel,
  updateCouch,
  closeModalWindow,
}: IUpdateCouchProps) {
  const [firstName, setFirstName] = useState(coachModel.firstName);
  const [lastName, setLastName] = useState(coachModel.lastName);
  const [description, setDescription] = useState(coachModel.description);
  const [error, setError] = useState<ValidationErrors<ICoachModel>>();
  const coachValidator = new CoachModelValidator();

  const submitHandler = async (event: React.FormEvent) => {
    event.preventDefault();
    setError({});

    const errorFromInput = coachValidator.validate({
      firstName: firstName,
      lastName: lastName,
      description: description,
    });
    console.log(errorFromInput);
    if (errorFromInput) {
      setError(errorFromInput);
    } else {
      updateCouch({
        id: coachModel.id,
        firstName: firstName,
        lastName: lastName,
        description: description,
      });
    }
    console.log(error);

    const newCoach: ICoachModel = {
      firstName: firstName,
      lastName: lastName,
      description: description,
    };
    console.log(error);

    console.log(newCoach);
  };

  const changeHandler = (
    inputField: string,
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    event.preventDefault();
    if (inputField === FIRST_NAME_INPUT) {
      console.log(event.target.value);
      setFirstName(event.target.value);
    } else if (inputField === LAST_NAME_INPUT) {
      console.log(event.target.value);
      setLastName(event.target.value);
    } else if (inputField === DESCRIPTION_INPUT) {
      setDescription(event.target.value);
    }
  };
  return (
    <>
      <form onSubmit={submitHandler}>
        <p> Id: {coachModel.id}</p>
        <input
          type="textr"
          className="border-solid hover:bg-gray-300 hover:placeholder:text-black
        py-2 px-4 mb-2 w-full "
          placeholder="Please, input name"
          value={firstName}
          onChange={(event) => {
            changeHandler(FIRST_NAME_INPUT, event);
          }}
        />
        <p>{error?.firstName && <ErrorMessage error={error.firstName} />}</p>
        <input
          type="textr"
          className="border-solid hover:bg-gray-300 hover:placeholder:text-black py-2 px-4 mb-2 w-full "
          placeholder="Please, input last name"
          value={lastName}
          onChange={(event) => {
            changeHandler(LAST_NAME_INPUT, event);
          }}
        />
        {error?.lastName && <ErrorMessage error={error.lastName} />}
        <input
          type="textr"
          className="border-solid hover:bg-gray-300 hover:placeholder:text-black py-2 px-4 mb-2 w-full "
          placeholder="Please, input name"
          value={description}
          onChange={(event) => changeHandler(DESCRIPTION_INPUT, event)}
        />
        <button
          type="submit"
          className="rounded-md py-2 px-4 border bg-blue-300 hover:bg-blue-500"
          onClick={submitHandler}
        >
          Update
        </button>
        <button
          onClick={closeModalWindow}
          className="rounded-md py-2 px-4 border bg-blue-300 
        hover:bg-blue-500 "
        >
          Close
        </button>
      </form>
    </>
  );
}
