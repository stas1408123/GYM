import { useState } from "react";
import { ICoachModel } from "../../Interfaces/ICoachModel";
import { ErrorMessage } from "../ErrorMessage";
import {
  DESCRIPTION_INPUT,
  FIRST_NAME_INPUT,
  LAST_NAME_INPUT,
} from "../../constants/constantsForInput";
import { CoachModelValidator } from "../../fluentValidator/CoachModelValidator";
import { ValidationErrors } from "fluentvalidation-ts/dist/ValidationErrors";

interface ICreateCouchProps {
  onCreateCouch: (couch: ICoachModel) => void;
  closeModalWindow: () => void;
}

export function CreateCoach({
  onCreateCouch,
  closeModalWindow,
}: ICreateCouchProps) {
  // hooks for input
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState<ValidationErrors<ICoachModel>>();

  const coachValidator = new CoachModelValidator();
  coachValidator.validate({ lastName: "", firstName: "", description: "" });

  const changeHandler = (
    inputField: string,
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
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

  const submitHandler = async (event: React.FormEvent) => {
    event.preventDefault();
    setError({ firstName: "", lastName: "", description: "" });
    const error = coachValidator.validate({
      firstName: firstName,
      lastName: lastName,
      description: description,
    });
    if (error) {
      setError(error);
    } else {
      onCreateCouch({
        firstName: firstName,
        lastName: lastName,
        description: description,
      });
    }
    setError(error);
    console.log(error);
  };

  return (
    <form onSubmit={submitHandler}>
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
        Create
      </button>
      <button
        onClick={closeModalWindow}
        className="rounded-md py-2 px-4 border bg-blue-300 
        hover:bg-blue-500 "
      >
        Close
      </button>
    </form>
  );
}
