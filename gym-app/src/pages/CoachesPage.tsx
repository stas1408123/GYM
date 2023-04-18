import { useContext } from "react";
import { CoachService } from "../services/coachService";
import { ModalContext } from "../context/ModalContext";
import { CoachComponent } from "../components/coachComponents/CoachComponent";
import { CreateCoach } from "../components/coachComponents/CreateCouch";
import { Modal } from "../components/Modal";

export function CoachesPage() {
  const { coaches, error, loading, addCoach } = CoachService();
  const { modal, open, close } = useContext(ModalContext);

  return (
    <>
      <h1>CouchPage</h1>

      <div className="container mx-auto grid grid-cols-3 gap-1">
        {coaches.map((element) => (
          <CoachComponent currentCoach={element} key={element.id} />
        ))}

        {modal && (
          <Modal title="Create new coach" onClose={close}>
            <CreateCoach
              onCreateCouch={addCoach}
              closeModalWindow={close}
            ></CreateCoach>
          </Modal>
        )}
        <button
          className="fixed bottom-10 right-10 rounded-full bg-red-700 text-white text-2xl px-4 py-2"
          onClick={open}
        >
          +
        </button>
      </div>
    </>
  );
}
