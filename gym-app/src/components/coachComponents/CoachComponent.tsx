import { useState } from "react";
import { ICoachModel } from "../../Interfaces/ICoachModel";
import { CoachService } from "../../services/coachService";
import { UpdateCoach } from "./UpdateCoach";
import { Modal } from "../Modal";
import { ErrorMessage } from "../ErrorMessage";

interface CoachComponentProps {
  currentCoach: ICoachModel;
}
export function CoachComponent({ currentCoach }: CoachComponentProps) {
  const { updateCoachPutRequest, deleteCoachDeleteRequest } = CoachService();
  const [errorWithId, setErrorWithId] = useState("");
  const [visibleUpdateForm, setVisibleUpdateForm] = useState(false);

  function changeVisibleUpdateWindow() {
    setVisibleUpdateForm(!visibleUpdateForm);
  }

  function deleteHandler(id: number | undefined) {
    if (id) {
      deleteCoachDeleteRequest(id);
    } else {
      setErrorWithId("Invalid Id.");
    }
  }

  return (
    <div className="container mx-auto max-w-2xl p-5 basis-1/3 bg-gray-300 rounded-md">
      <p>Id: {currentCoach.id}</p>
      {errorWithId && <ErrorMessage error={errorWithId} />}
      <p>Name: {currentCoach.firstName}</p>
      <p>Last name: {currentCoach.lastName}</p>
      <p>Description: {currentCoach.description}</p>
      <p>quantity clients: {currentCoach.visitors?.length}</p>

      {visibleUpdateForm && (
        <Modal title="Update coach" onClose={changeVisibleUpdateWindow}>
          <UpdateCoach
            coachModel={currentCoach}
            updateCouch={updateCoachPutRequest}
            closeModalWindow={changeVisibleUpdateWindow}
          ></UpdateCoach>
        </Modal>
      )}

      <button
        className="rounded-md py-2 px-4 border bg-blue-300 hover:bg-blue-500"
        onClick={changeVisibleUpdateWindow}
      >
        Update
      </button>
      <button
        className="rounded-md py-2 px-4 border bg-red-300 hover:bg-red-500"
        onClick={(event) => {
          deleteHandler(currentCoach.id);
        }}
      >
        Delete
      </button>
    </div>
  );
}
