import { useEffect, useState } from "react"
import { ICoachModel } from "../Interfaces/ICoachModel"
import axios, { AxiosError } from "axios"
import {
  ROUTE_FOR_COACHES_TO_GYM_API_WITHOUT_ID,
  ROUTE_FOR_COACHES_TO_GYM_API_WITH_ID
} from "../constants/RouteConstants"

export function CoachService(){
    const [coaches, setCoaches] = useState<ICoachModel[]>([])
    const [loading,setloading] = useState(false)
    const [error,setError] = useState('')

    function addCoach(product: ICoachModel) {
    setCoaches(prev => [...prev, product])
  }
  
//for test
  // function fetchTestCoaches() {
  //   setCoaches(coachesForTest)
  // }

  async function addCoachPostRequest(coach:ICoachModel){
    const response = await axios.post(ROUTE_FOR_COACHES_TO_GYM_API_WITHOUT_ID,coach)
    return response.status
  }

   async function updateCoachPutRequest(coach:ICoachModel){
    const response = await axios.put(ROUTE_FOR_COACHES_TO_GYM_API_WITHOUT_ID,coach)
    return response.status
  }

   async function deleteCoachDeleteRequest(id:number){
    const response = await axios.delete(ROUTE_FOR_COACHES_TO_GYM_API_WITH_ID.concat("/",id.toString()))
    return response.status
  }

  async function fetchCoaches() {
    try {
        setError('')
        setloading(true)
        const response = await axios.get<ICoachModel[]>("path")
        setCoaches(response.data)
        
    } catch (ex : unknown) {
        const error = ex as AxiosError
        setError(error.message)
    }
  }

   useEffect(()=>{
    
    fetchCoaches() 
    
    //For testing
    //fetchTestCoaches() 
   },[])

return {coaches,error,loading,addCoach,addCoachPostRequest,updateCoachPutRequest,deleteCoachDeleteRequest}
}