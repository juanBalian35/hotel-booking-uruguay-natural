import {CategoryBasicInfo} from './category-basic-info';
import {RegionBasicInfo} from './region-basic-info';

export interface TouristspotBasicInfo {
    id: number;
    name: string;
    categories: CategoryBasicInfo[];
    imageData: Uint8Array;

}

