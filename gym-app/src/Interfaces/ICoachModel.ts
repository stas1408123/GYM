export interface ICoachModel{
    id?: number;
  firstName: string;
  lastName: string;
  description: string;
  visitors?: {
    visitorId: number;
    visitorFirstName: string;
    visitorLastName: string;
  }[];
}