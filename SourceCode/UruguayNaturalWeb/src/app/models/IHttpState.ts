import {HttpProgressState} from './http-progress-state';

export interface IHttpState {
    url: string;
    state: HttpProgressState;
}
